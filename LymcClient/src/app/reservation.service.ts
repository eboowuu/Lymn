import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';
import { Reservation } from './reservation'


@Injectable()
export class ReservationService {

  private BASE_URL = "https://localhost:44368/api/ReservationsApi";

  constructor(private http: Http) { }

  getReservations(): Promise<Reservation[]> {
    return this.http.get(this.BASE_URL)
      .toPromise()
      .then(response => response.json() as Reservation[])
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occured', error);
    return Promise.reject(error.message || error);
  }

}
