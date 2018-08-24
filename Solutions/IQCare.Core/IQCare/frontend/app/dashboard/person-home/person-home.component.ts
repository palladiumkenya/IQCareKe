import { Component, OnInit } from '@angular/core';
import { ActivatedRoute  } from '@angular/router';
import {Subscription} from 'rxjs';
import {PersonHomeService} from '../services/person-home.service';
import {NotificationService} from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import {PersonView} from '../../records/_models/personView';

@Component({
  selector: 'app-person-home',
  templateUrl: './person-home.component.html',
  styleUrls: ['./person-home.component.css']
})
export class PersonHomeComponent implements OnInit {

    [x: string]: any;

    public personId = 0;
    public person: PersonView;
    public personView$: Subscription;
  constructor(private route: ActivatedRoute, private personService: PersonHomeService, private snotifyService: SnotifyService,
              private notificationService: NotificationService) { }

  ngOnInit() {
      this.route.params.subscribe(params => {
          this.personId = params['id'];
          console.log('personId' + this.personId);
      });

      this.getPatientDetilsById(this.personId);
  }

  public getPatientDetilsById(personId: number) {
      this.personView$ = this.personService.getPatientByPersonId(personId)
          .subscribe(
              p => {
                  console.log(p);
                  this.person = p;
                  console.log('person');
                  console.log(this.person);
              },
              (err) => {
                  console.log(err);
                  this.snotifyService.error('Error editing encounter ' + err, 'person detail service',
                   this.notificationService.getConfig());
              },
              () => {
                  console.log(this.personView$);
              });
  }

}
