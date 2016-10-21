import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';

export class Legion {
    constructor(public guid: string, public expires: string, public issuer: string, public team: Person[]){ }
}

export class Person {
    constructor(public firstName: string, public lastName: string, public heroName: string){ }
}

@Injectable()
export class LegionService {
    constructor(private http: Http){ }

    getLegion(){
        return this.http
        .get('api/legion.json')
        //.get('api/legion/5')
        .map((response: Response)=> <Legion>response.json().data)
        .do(data => console.log(data))
        .catch(this.handleError);
    }

 private handleError(error: Response) {
    console.error(error);
    let msg = `Error status code ${error.status} at ${error.url}`;
    return Observable.throw(msg);
  }
}