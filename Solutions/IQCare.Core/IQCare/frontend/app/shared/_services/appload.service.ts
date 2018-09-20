
import {map, catchError, tap} from 'rxjs/operators';
import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';


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
            this.http.get(this.API_URL + this.url + '/getFacilityList').pipe(
                map(res => res))
                .subscribe(response => {
                    this.facilities = response['facilityList'];
                    resolve(true);
                });
        });
    }
}
