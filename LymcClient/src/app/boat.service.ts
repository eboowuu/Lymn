import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';
import { Boat } from './boat';

@Injectable()
export class BoatService {


  private BASE_URL = "https://wunesvaderaniassignment02.azurewebsites.net/api/BoatsApi";
  constructor(private http: Http) { }

  getBoats(): Promise<Boat[]> {
    return this.http.get(this.BASE_URL)
      .toPromise()
      .then(response => response.json() as Boat[])
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occured', error);
    return Promise.reject(error.message || error);
  }
}
