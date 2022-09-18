import { Component, Input, OnInit } from '@angular/core';
import { Destination } from 'src/app/_models/destination';

@Component({
  selector: 'app-photo-detination-editor',
  templateUrl: './photo-detination-editor.component.html',
  styleUrls: ['./photo-detination-editor.component.css']
})
export class PhotoDetinationEditorComponent implements OnInit {
  @Input() destination: Destination;
  constructor() { }

  ngOnInit(): void {
  }

}
