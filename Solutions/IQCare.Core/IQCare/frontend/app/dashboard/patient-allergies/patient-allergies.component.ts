
import { Component, OnInit, ViewChild } from '@angular/core';
import {MatPaginator, MatTableDataSource } from '@angular/material';
import { PersonHomeService } from '../services/person-home.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-patient-allergies',
  templateUrl: './patient-allergies.component.html',
  styleUrls: ['./patient-allergies.component.css']
})
export class PatientAllergiesComponent implements OnInit {
  personId = 0;
  patient_allergy_data: any[] = [];
  dataSource = new MatTableDataSource(this.patient_allergy_data);
  patient_allergy_displaycolumns = [ 'allagen', 'description', 'dateCreated'];
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private personService: PersonHomeService, private route: ActivatedRoute) {

   }

  ngOnInit() {
     this.route.params.subscribe(params => {
     this.personId = params['id'];
      console.log("PersonId >>>> "+this.personId)

     this.personService.getPatientByPersonId(this.personId).subscribe(patient => {
      console.log("Patient Id >>>>>" +patient.patientId)
       this.getPatientAllergies(patient.patientId);
    });
    });
  }

  public getPatientAllergies(patientId: number) {
    if(patientId == null)
    return;
    this.personService.getPatientAllergies(patientId).subscribe(
        data => {
          if (data.length == 0) {
            return;
        }
            data.forEach(allergy => {
              this.patient_allergy_data.push({
             
                allagen: allergy.allagen,
                dateCreated: allergy.dateCreated,
                description: allergy.description
                
            });
            });

     
         this.dataSource = new MatTableDataSource(this.patient_allergy_data);
         this.dataSource.paginator = this.paginator;
        }, (err) => {
           console.log(err);
        }
    );
}

}
