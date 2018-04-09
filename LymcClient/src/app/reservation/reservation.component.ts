import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Reservation } from '../reservation';
import { ReservationService } from '../reservation.service';
import { BoatService } from '../boat.service';
import { Boat } from '../boat';

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.css']
})
export class ReservationComponent implements OnInit {

  constructor(private reservationService: ReservationService, private boatService: BoatService, private route: Router) {
    this.newReservation = new Reservation();
    this.newReservation.reservedBoat = new Boat();
  }

  newReservation: Reservation;
  boats: Boat[];

  add(newReservation: Reservation): void {
    newReservation.userName = newReservation.userName.trim();

    if (!newReservation) { return; }
    this.reservationService.createReservation(newReservation);

    location.reload();
  }


  getBoats(): void {
    this.boatService.getBoats()
      .then(results => this.boats = results);
  }

  ngOnInit() {
    this.getBoats();
  }

 
}
