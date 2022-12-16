import { Component, OnInit } from '@angular/core';
import { LandingService } from '../landing.service';
import { About } from '../_models/about';
import { Feedback } from '../_models/feedback';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss'],
})
export class AboutComponent implements OnInit {
  abouts: About[] = [];
  feedbacks: Feedback[] = [];
  ite: number = 0;
  time: number = 5000;
  interval: any;

  constructor(private landingService: LandingService) {}

  ngOnInit(): void {
    this.loadAbouts();
    this.loadFeedbacks();
  }

  loadAbouts() {
    this.landingService.getAbouts().subscribe({
      next: (abouts) => {
        this.abouts = abouts;
      },
    });
  }

  loadFeedbacks() {
    this.landingService.getFeedbacks().subscribe({
      next: (feedbacks) => {
        this.feedbacks = feedbacks;
      },
    });
  }

  forwardList() {
    if (this.ite < this.feedbacks.length - 1) {
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
      this.ite = this.feedbacks.length - 1;
    }
    clearInterval(this.interval);
    this.timer();
  }

  timer() {
    this.interval = setInterval(() => {
      if (this.ite < this.feedbacks.length - 1) {
        this.ite++;
      } else {
        this.ite = 0;
      }
    }, this.time);
  }
}
