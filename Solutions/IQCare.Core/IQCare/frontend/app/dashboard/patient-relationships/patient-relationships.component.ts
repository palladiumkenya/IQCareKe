import { Component, OnInit, ViewChild, NgZone } from '@angular/core';
import { PersonHomeService } from '../services/person-home.service';
import {MatPaginator, MatTableDataSource } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';



@Component({
  selector: 'app-patient-relationships',
  templateUrl: './patient-relationships.component.html',
  styleUrls: ['./patient-relationships.component.css']
})
export class PatientRelationshipsComponent implements OnInit {
  personId = 0;
  patient_relationships : any[] = [];
  dataSource = new MatTableDataSource(this.patient_relationships);
  patient_relationships_columns = ['name','relationship','sex','relativepatientid'];
  @ViewChild(MatPaginator) paginator : MatPaginator;

  constructor(private personService: PersonHomeService, private route : ActivatedRoute,
    private router: Router,public zone: NgZone)
   { 

  }

  ngOnInit() {
     this.route.params.subscribe(params=>{
       this.personId = params['id'];
       this.personService.getPatientByPersonId(this.personId).subscribe(
         patient=>{
           this.getRelationshipsByPatientId(patient.patientId)
       })
     })
  }

  public getRelationshipsByPatientId(patientId:number){
    if(patientId == null)
    return;
    this.personService.getRelationshipsByPatientId(patientId).subscribe(
      data=>{
          if(data.length == 0)
            return;
           this.patient_relationships  = [];
            data.forEach(relationship => {
              this.patient_relationships.push(
              {
                name : relationship.relativeName,
                relationship : relationship.relationship,
                sex: relationship.relativeSex,
                relativepatientid: relationship.relativePatientId,
                relativepersonId : relationship.relativePersonId         
              });
            });
            this.dataSource = new MatTableDataSource(this.patient_relationships);
            this.dataSource.paginator = this.paginator;
    },
      (err)=>{
         console.log(err);
    })
  }

  public redirectToHomePage(relationship: any) {
    var personId = relationship["relativepersonId"]
    this.zone.run(() => { this.router.navigate(['/dashboard/personhome/' + personId], { relativeTo: this.route }); });
}

}
