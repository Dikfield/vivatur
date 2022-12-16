import { Component, Input, OnInit } from '@angular/core';
import { Destination } from '../_models/destination';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss'],
})
export class FooterComponent implements OnInit{
  @Input() destinations: Destination[] = [];

  constructor() {}

  ngOnInit(): void {
    this.destinations;
  }
}
