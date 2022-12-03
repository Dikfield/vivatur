import { animation, animate, style, transition, trigger, useAnimation } from '@angular/animations';
import { Component } from '@angular/core';
import { LandingService } from '../landing.service';
import { Destination } from '../_models/destination';

export const scaleIn = animation([
  style({ opacity: 0, transform: "scale(0.5)" }), // start state
  animate(
    "{{time}} cubic-bezier(0.785, 0.135, 0.15, 0.86)",
    style({ opacity: 1, transform: "scale(1)" })
  )
]);

export const scaleOut = animation([
  animate(
    "{{time}} cubic-bezier(0.785, 0.135, 0.15, 0.86)",
    style({ opacity: 0, transform: "scale(0.5)" })
  )
]);

@Component({
  selector: 'app-destination',
  templateUrl: './destination.component.html',
  styleUrls: ['./destination.component.scss'],
  animations: [
    trigger("slideAnimation", [
      /* scale */
      transition("void => *", [useAnimation(scaleIn, {params: { time: '500ms' }} )]),
      transition("* => void", [useAnimation(scaleOut, {params: { time: '500ms' }})]),
    ])
  ]
})
export class DestinationComponent {
  destinations: Destination[] = [];
  ite: number = 0;
  time:number = 5000;
  interval: any;

  constructor(private landingService: LandingService) {}

  ngOnInit(): void {
    this.initialState();
    this.timer();
  }

  initialState() {
    this.landingService.getDestinations().subscribe({
      next: (destinations) => {
        this.destinations = destinations;
      },
    });
  }

  timer() {
      this.interval = setInterval(() => {
        if(this.ite < this.destinations.length -1){
         this.ite++;
        } else {
          this.ite=0;
        }

      }, this.time);

  }

  forwardList() {
    if (this.ite < this.destinations.length - 1) {
      this.ite++;
    } else {
      this.ite =0;
    }

    clearInterval(this.interval);
    this.timer();
  }

  backwardList() {
    if (this.ite > 0) {
      this.ite--;
    } else {
      this.ite = this.destinations.length -1;
    }
    clearInterval(this.interval);
    this.timer();
  }

  fadeIn = animation([
    style({ opacity: 0 }), // start state
    animate('300ms', style({ opacity: 1 }))
  ]);

  fadeOut = animation([
    animate('300ms', style({ opacity: 0 }))
  ]);


}


