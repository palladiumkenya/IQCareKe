import { LookupItemView } from './../../shared/_models/LookupItemView';
import { ErrorHandlerService } from './../../shared/_services/errorhandler.service';
import { FormControlBase } from './../../shared/_models/FormControlBase';
import { Injectable } from '@angular/core';
import { DropdownFormControl } from '../../shared/_models/FormControl-Dropbox';
import { TextboxFormControl } from '../../shared/_models/FormControl-Textbox';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { tap, catchError } from 'rxjs/operators';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class FamilyPartnerControlsService {
    private API_URL = environment.API_URL;

    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) { }

    public getRelationshipTypes() {
        return this.http.get<any[]>(this.API_URL + '/api/Lookup/GetbyGroupName/Relationship', httpOptions).pipe(
            tap(getRelationshipTypes => this.errorHandler.log('get relationship types')),
            catchError(this.errorHandler.handleError<any[]>('getRelationshipTypes'))
        );
    }
}
