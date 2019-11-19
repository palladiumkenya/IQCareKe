import { PrepService } from '../prep.service';
import { Observable } from 'rxjs';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import * as moment from 'moment';

@Injectable()
export class FormSettingsResolver implements Resolve<Observable<any[]>>
{

    public patientId = 0;
    public VisitCheckinDate: Date;
    public visitDate: string;
    public EmrMode: string;


    constructor(private prepService: PrepService) {
    }


    public resolve(
        route: ActivatedRouteSnapshot, state: RouterStateSnapshot
    ): Observable<any[]> {
        this.patientId = route.params['patientId'];
        this.visitDate = localStorage.getItem('PrepVisitDate').toString();
        
        this.VisitCheckinDate = moment(moment(this.visitDate).format('DD-MMM-YYYY')).toDate();
        console.log(this.VisitCheckinDate);
        this.EmrMode = localStorage.getItem('PrepCheckinEmrMode').toString();
        return this.prepService.getCorrectDisplayForm(this.patientId, this.VisitCheckinDate, this.EmrMode);
    }
}