import { Component, OnInit } from '@angular/core';
import { Reservation } from './reservation'
import { ReservationService } from './reservation.service'
import { ActivatedRoute } from '@angular/router';
import { BoatService } from './boat.service';
import { Boat } from './boat';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [ReservationService, BoatService]
})

export class AppComponent implements OnInit {
  reservations: Reservation[];
  currentUser: string;

  constructor(private resService: ReservationService) { }

  getReservations(): void {
    this.resService.getReservations()
      .then(results => this.reservations = results);
  }

  

  ngOnInit(): void {
    this.getReservations();
    
  }
}


