import { Component, OnInit } from '@angular/core';
import { LandingService } from '../landing.service';
import { About } from '../_models/about';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss'],
})
export class AboutComponent implements OnInit {
  abouts: About[] = [];

  constructor(private landingService: LandingService) {}

  ngOnInit(): void {
    this.loadAbouts();
  }

  loadAbouts() {
    this.landingService.getAbouts().subscribe({
      next: (abouts) => {
        this.abouts = abouts;
      },
    });
  }
}
