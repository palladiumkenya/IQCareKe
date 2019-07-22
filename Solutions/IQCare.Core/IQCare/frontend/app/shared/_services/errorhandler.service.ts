
import { throwError as observableThrowError, Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable()
export class ErrorHandlerService {
    constructor() { }

    public handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {

            // TODO: send the error to remote logging infrastructure
            console.error(error); // log to console instead

            // TODO: better job of transforming error for user consumption
            this.log(`${operation} failed: ${error.message}`);
            let message = '';
            for (let i = 0; i < error.error.errors.length; i++) {
                message += error.error.errors[i].message;
            }
            return observableThrowError(message);
        };
    }

    /** Log a HeroService message with the MessageService */
    public log(message: string) {
        // tslint:disable-next-line:no-console
        // console.info(message);
    }
}
