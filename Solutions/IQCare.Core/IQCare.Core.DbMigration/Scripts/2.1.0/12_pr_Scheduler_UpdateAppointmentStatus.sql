ALTER PROCEDURE [dbo].[pr_Scheduler_UpdateAppointmentStatus]
(@Currentdate DATETIME,
 @locationid  INT
)
AS      
     --Begin------Check in PatientMasterVisit table if record exist(excluding the records of scheduler and enrollment visit type)      ------with in grace period of appointment date then update the status to met if the       ------appointment date + grace period has gone then update the status to missed      ------seprately check the record in lab order table as the lab entries does not go into the PatientMasterVisit table      -----------------------Update Met status--------------------------      
     UPDATE PatientAppointment
       SET
           StatusId =
     (
		SELECT ItemId FROM LookupItemView WHERE MasterName='AppointmentStatus' AND ItemName = 'Met'
     ),
           StatusDate =
     (
         SELECT MIN(VisitDate)
         FROM PatientMasterVisit c
         WHERE(c.visitdate BETWEEN(PatientAppointment.AppointmentDate -
                                  (
                                      SELECT appgraceperiod
                                      FROM mst_facility
                                      WHERE facilityid = @locationid
                                  )) AND(PatientAppointment.AppointmentDate +
                                        (
                                            SELECT appgraceperiod
                                            FROM mst_facility
                                            WHERE facilityid = @locationid
                                        ) + 1))
              AND c.PatientId = PatientAppointment.PatientId
              --AND c.Id <> PatientAppointment.PatientMasterVisitId
         --     AND visittype NOT IN(5, 0)
         --AND visittype <> 0
     )
     WHERE StatusId =
     (
		 SELECT ItemId FROM LookupItemView WHERE MasterName='AppointmentStatus' AND ItemName = 'Pending'
     )
           AND (deleteflag IS NULL
                OR deleteflag != 1)
           AND PatientId IN
     (
         SELECT c.PatientId
         FROM PatientMasterVisit c
         WHERE((c.visitdate BETWEEN(PatientAppointment.AppointmentDate -
                                   (
                                       SELECT appgraceperiod
                                       FROM mst_facility
                                       WHERE facilityid = @locationid
                                   )) AND(PatientAppointment.AppointmentDate +
                                         (
                                             SELECT appgraceperiod
                                             FROM mst_facility
                                             WHERE facilityid = @locationid
                                         ) + 1)))
              AND c.PatientId = PatientAppointment.PatientId
         --     AND c.Id <> PatientAppointment.PatientMasterVisitId
         --     AND visittype NOT IN(5, 0)
         --AND visittype <> 0
     );
     
     ---- -----------------------Update Missed status--------------------------      
     UPDATE PatientAppointment
       SET
           StatusId =
     (
		 SELECT ItemId FROM LookupItemView WHERE MasterName='AppointmentStatus' AND ItemName = 'Missed'
     )
     WHERE StatusId =
     (
		 SELECT ItemId FROM LookupItemView WHERE MasterName='AppointmentStatus' AND ItemName = 'Pending'
     )
           AND (deleteflag IS NULL
                OR deleteflag != 1)
		   AND PatientAppointment.AppointmentDate -
                   (
                       SELECT appgraceperiod
                       FROM mst_facility
                       WHERE facilityid = @locationid
                   ) < @currentdate

           AND PatientId IN
     (
         SELECT PatientAppointment.PatientId
         FROM PatientAppointment
         WHERE StatusId =
         (
			 SELECT ItemId FROM LookupItemView WHERE MasterName='AppointmentStatus' AND ItemName = 'Pending'
         )
               AND PatientId = PatientAppointment.PatientId
               --AND StatusDate = PatientAppointment.StatusDate
               AND (PatientAppointment.AppointmentDate +
                   (
                       SELECT appgraceperiod
                       FROM mst_facility
                       WHERE facilityid = @locationid
                   ) < @currentdate)
     );

     UPDATE A
       SET
           StatusId =
     (
         SELECT ItemId FROM LookupItemView WHERE MasterName='AppointmentStatus' AND ItemName = 'CareEnded'
     )
     FROM Patient AS P
		  INNER JOIN PatientEnrollment PE ON PE.PatientId = P.Id
          INNER JOIN PatientAppointment AS A ON P.Id = A.PatientId
     WHERE(PE.CareEnded = 1 AND PE.ServiceAreaId = 1)
          AND A.DeleteFlag = 0
          AND A.StatusId IN
     (
     (
		 SELECT ItemId FROM LookupItemView WHERE MasterName='AppointmentStatus' AND ItemName = 'Pending'
     )
     );
     ----update status of all those active patients(previously inactive) who have careended appointments, to missed and      
	 -----Then compare StatusDate with currentdate if curentdate is less then (StatusDate + graceperoiddate) then mark StatusId pending      
     UPDATE PatientAppointment
       SET
           StatusId =
     (
		 SELECT ItemId FROM LookupItemView WHERE MasterName='AppointmentStatus' AND ItemName = 'Pending'
     )
     WHERE StatusId =
     (
		 SELECT ItemId FROM LookupItemView WHERE MasterName='AppointmentStatus' AND ItemName = 'CareEnded'
     )
	 AND 
	 (PatientAppointment.AppointmentDate +
                   (
                       SELECT appgraceperiod
                       FROM mst_facility
                       WHERE facilityid = @locationid
                   ) >= @currentdate)
           AND (deleteflag IS NULL
                OR deleteflag != 1)
           AND PatientId IN
     (
         SELECT PatientId
         FROM Patient P
		 INNER JOIN PatientEnrollment PE ON PE.PatientId = P.Id
         WHERE PE.CareEnded = 0 AND PE.ServiceAreaId = 1
     );
     --End