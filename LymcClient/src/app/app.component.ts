import { Component, OnInit } from '@angular/core';
import { Reservation } from './reservation'
import { ReservationService } from './reservation.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [ReservationService]
})

export class AppComponent implements OnInit {
  reservations: Reservation[];

  constructor(private resService: ReservationService) { }

  getReservations(): void {
    this.resService.getReservations()
      .then(results => this.reservations = results);
  }

  ngOnInit(): void {
    this.getReservations();
  }
}


