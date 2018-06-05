import { Injectable } from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {catchError, tap} from 'rxjs/operators';
import 'rxjs/add/operator/map';

@Injectable()
export class AppLoadService {
    private API_URL = environment.API_URL;
    private url = '/api/Lookup';
    private facilities: any[] = [];

    constructor(private http: HttpClient) { }

    public getFacilities() {
        return this.facilities;
    }

    loadFacilities() {
        return new Promise((resolve, reject) => {
            this.http.get(this.API_URL + this.url + '/getFacilityList')
                .map(res => res)
                .subscribe(response => {
                    this.facilities = response['facilityList'];
                    resolve(true);
                });
        });
    }
}
