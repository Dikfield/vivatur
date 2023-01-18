import { Component } from '@angular/core';
import { LandingService } from '../landing.service';
import { Promotion } from '../_models/promotion';

@Component({
  selector: 'app-package',
  templateUrl: './package.component.html',
  styleUrls: ['./package.component.scss'],
})
export class PackageComponent {
  promotions: Promotion[] = [];
  ite: number = 0;
  time: number = 5000;
  interval: any;

  constructor(private landingService: LandingService) {}

  ngOnInit(): void {
    this.initialState();
    this.timer();
  }

  initialState() {
    this.landingService.getPromotions().subscribe({
      next: (promotions) => {
        this.promotions = promotions.filter(promotions => promotions.photoUrl !== null);
      },
    });
  }

  timer() {
    this.interval = setInterval(() => {
      if (this.ite < this.promotions.length - 1) {
        this.ite++;
      } else {
        this.ite = 0;
      }
    }, this.time);
  }

  forwardList() {
    if (this.ite < this.promotions.length - 1) {
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
      this.ite = this.promotions.length - 1;
    }
    clearInterval(this.interval);
    this.timer();
  }
}
