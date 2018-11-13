import { Component, OnInit, ViewChild } from '@angular/core';
import {MatPaginator, MatTableDataSource } from '@angular/material';
import { PersonHomeService } from '../services/person-home.service';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-chronich-illness',
  templateUrl: './chronich-illness.component.html',
  styleUrls: ['./chronich-illness.component.css']
})
export class ChronichIllnessComponent implements OnInit {
  personId = 0;
  chronic_illness_data: any[] = [];
  dataSource = new MatTableDataSource(this.chronic_illness_data);
  chronic_illness_displaycolumns = ['illness', 'onsetdate', 'treatment', 'dose'];
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private personService: PersonHomeService, private route: ActivatedRoute) {

   }

  ngOnInit() {
     this.route.params.subscribe(params => {
     this.personId = params['id'];
     this.personService.getPatientByPersonId(this.personId).subscribe(patient => {
       this.getChronicIllnessesByPatientId(patient.patientId);
    });
    });
  }

  public getChronicIllnessesByPatientId(patientId: number) {
    if(patientId == null)
    return;
    this.personService.getChronicIllnessesByPatientId(patientId).subscribe(
        data => {
          if (data.length == 0) {
            return;
        }
            data.forEach(illness => {
              this.chronic_illness_data.push({
                illness: illness.chronicIllness,
                onsetdate: illness.onsetDate,
                treatment: illness.treatment,
                dose: illness.dose
            });
            });
     
         this.dataSource = new MatTableDataSource(this.chronic_illness_data);
         this.dataSource.paginator = this.paginator;
        }, (err) => {
           console.log(err);
        }
    );
}

}
