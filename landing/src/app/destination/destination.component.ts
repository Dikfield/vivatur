import { Component } from '@angular/core';
import { LandingService } from '../landing.service';
import { Destination } from '../_models/destination';

@Component({
  selector: 'app-destination',
  templateUrl: './destination.component.html',
  styleUrls: ['./destination.component.scss'],
})
export class DestinationComponent {
  destinations!: Destination[];
  ite: number = 0;
  time: number = 5000;
  interval: any;

  constructor(private landingService: LandingService) {}

  ngOnInit(): void {
    this.initialState();
    this.timer();
  }

  initialState() {
    this.landingService.getDestinations().subscribe({
      next: (destinations) => {
        this.destinations = destinations.filter(destinations => destinations.photoUrl !== null);
      },
    });
  }

  timer() {
    this.interval = setInterval(() => {
      if (this.ite < this.destinations.length - 1) {
        this.ite++;
      } else {
        this.ite = 0;
      }
    }, this.time);
  }

  forwardList() {
    if (this.ite < this.destinations.length - 1 ) {
      this.ite++;
    } else {
      this.ite = 0;
    }

    clearInterval(this.interval);
    this.timer();
  }

  backwardList() {
    if (this.ite > 0) {
      this.ite--;
    } else {
      this.ite = this.destinations.length - 1;
    }
    clearInterval(this.interval);
    this.timer();
  }
}
