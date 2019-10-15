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
  patient_relationships: any[] = [];
  dataSource = new MatTableDataSource(this.patient_relationships);
  patient_relationships_columns = ['name', 'relationship', 'sex', 'relativepatientid'];
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private personService: PersonHomeService, private route: ActivatedRoute,
    private router: Router, public zone: NgZone) {

  }

  async ngOnInit() {
     this.route.params.subscribe(params => {
       this.personId = params['id'];
       this.personService.getPatientByPersonId(this.personId).subscribe(
         patient => {
           this.getRelationshipsByPatientId(patient.patientId);
       });
     });

     await this.getSecondaryRelationships();
  }

  public getRelationshipsByPatientId(patientId: number) {
    if (patientId == null) {
        return;
    }

    this.personService.getRelationshipsByPatientId(patientId).subscribe(
      data => {
          if (data.length == 0) {
              return;
          }

           this.patient_relationships  = [];
            data.forEach(relationship => {
              this.patient_relationships.push({
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
      (err) => {
         console.log(err);
    });
  }

  public async getSecondaryRelationships() {
      const secondaryRelationships = await this.personService.getSecondaryRelationships(this.personId).toPromise();
      for (let i = 0; i < secondaryRelationships.length; i++) {
          let relationship = '';
          switch (secondaryRelationships[i]['relationship'].toString().toLowerCase()) {
              case 'mother':
                  relationship = 'child';
                  break;
              case 'spouse':
                  relationship = 'spouse';
                  break;
              case 'other':
                  relationship = 'spouse';
                  break;
              case 'sibling':
                  relationship = 'sibling';
                  break;
              case 'partner':
                  relationship = 'partner';
                  break;
              case 'child':
                  relationship = 'parent';
                  break;
              case 'father':
                  relationship = 'father';
                  break;
              case 'co-wife':
                  relationship = 'co-wife';
                  break;
              default:
                  relationship = 'other';
                  break;
          }

          this.patient_relationships.push({
              name : secondaryRelationships[i].patientName,
              relationship : relationship,
              sex: secondaryRelationships[i].patientSex,
              relativepatientid: secondaryRelationships[i].patientId,
              relativepersonId : secondaryRelationships[i].patientPersonId
          });
          this.dataSource = new MatTableDataSource(this.patient_relationships);
          this.dataSource.paginator = this.paginator;

      }
  }
}
