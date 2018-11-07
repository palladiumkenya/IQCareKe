import { Component, OnInit, ViewChild } from '@angular/core';
import { PersonHomeService } from '../services/person-home.service';
import {MatPaginator, MatTableDataSource } from '@angular/material';
import { ActivatedRoute } from '@angular/router';



@Component({
  selector: 'app-patient-relationships',
  templateUrl: './patient-relationships.component.html',
  styleUrls: ['./patient-relationships.component.css']
})
export class PatientRelationshipsComponent implements OnInit {
  personId = 0;
  patient_relationships : any[] = [];
  dataSource = new MatTableDataSource(this.patient_relationships);
  patient_relationships_columns = ['relationship','sex','name'];
  @ViewChild(MatPaginator) paginator : MatPaginator;

  constructor(private personService: PersonHomeService, private route : ActivatedRoute) { }

  ngOnInit() {
     this.route.params.subscribe(params=>{
       this.personId = params['id'];
       console.log("personId >>>"+ this.personId)
       this.personService.getPatientByPersonId(this.personId).subscribe(
         patient=>{
           this.getRelationshipsByPatientId(patient.patientId)
       })
     })
  }

  public getRelationshipsByPatientId(patientId:number){
    this.personService.getRelationshipsByPatientId(patientId).subscribe(
      data=>{
        console.log(data);

          if(data.length == 0)
            return;
            data.forEach(relationship => {
              this.patient_relationships.push(
              {
                name : relationship.relativeName,
                relationship : relationship.relationship,
                sex: relationship.relativeSex              
              });
            });
            this.dataSource = new MatTableDataSource(this.patient_relationships);
            this.dataSource.paginator = this.paginator;
    },
      (err)=>{

    })
  }

}
