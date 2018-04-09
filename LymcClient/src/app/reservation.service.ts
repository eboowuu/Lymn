import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';
import { Reservation } from './reservation'


@Injectable()
export class ReservationService {

  private BASE_URL = "https://wunesvaderaniassignment02.azurewebsites.net/api/ReservationsApi";
  private headers = new Headers({ 'Content-Type': 'application/json' });

  constructor(private http: Http) { }

  getReservations(): Promise<Reservation[]> {
    return this.http.get(this.BASE_URL)
      .toPromise()
      .then(response => response.json() as Reservation[])
      .catch(this.handleError);
  }

  createReservation(newReservation: Reservation): Promise<Reservation> {

    var test = JSON.stringify(newReservation);

    console.log(test);

    return this.http
      .post(this.BASE_URL, JSON.stringify(newReservation), { headers: this.headers })
      .toPromise()
      .then(res => res.json().data)
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occured', error);
    return Promise.reject(error.message || error);
  }

}
