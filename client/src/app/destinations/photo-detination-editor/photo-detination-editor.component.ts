import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Destination } from 'src/app/_models/destination';
import { DestinationPhoto } from 'src/app/_models/destinationPhoto';
import { DestinationsService } from 'src/app/_services/destinations.service';

@Component({
  selector: 'app-photo-detination-editor',
  templateUrl: './photo-detination-editor.component.html',
  styleUrls: ['./photo-detination-editor.component.css']
})
export class PhotoDetinationEditorComponent implements OnInit {
  @Input() destination: Destination;
  constructor(private destinationService:DestinationsService, private route: ActivatedRoute,
    private toastr:ToastrService) { }

  ngOnInit(): void {
  }

  setMainPhoto(name:string, photo:DestinationPhoto) {
    this.destinationService.setMainPhoto(name, photo.id).subscribe(() => {
      this.destination.photoUrl = photo.url;
      this.destination.destinationPhotos.forEach(p =>{
        if(p.isMain)p.isMain = false;
        if(p.id ===photo.id) p.isMain = true;
      })
    })
  }

  deletePhoto(photoId:number){
    this.destinationService.deletePhoto(photoId).subscribe(()=>{
      this.destination.destinationPhotos = this.destination.destinationPhotos.filter(x=>x.id !== photoId);
    })
  }

}
