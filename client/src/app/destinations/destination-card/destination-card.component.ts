import { Component, Input, OnInit } from '@angular/core';
import { Destination } from 'src/app/_models/destination';

@Component({
  selector: 'app-destination-card',
  templateUrl: './destination-card.component.html',
  styleUrls: ['./destination-card.component.css'],
  
})
export class DestinationCardComponent implements OnInit {
  @Input() destination:Destination;

  constructor() { }

  ngOnInit(): void {
  }

}
