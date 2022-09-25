import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { FileUploader } from 'ng2-file-upload';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { DescriptionPhoto } from 'src/app/_models/descriptionPhoto';
import { Destination } from 'src/app/_models/destination';
import { DestinationDescription } from 'src/app/_models/destinationDescription';
import { DestinationPhoto } from 'src/app/_models/destinationPhoto';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { DestinationsService } from 'src/app/_services/destinations.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-description',
  templateUrl: './description.component.html',
  styleUrls: ['./description.component.css']
})
export class DescriptionComponent implements OnInit {
  @Input() destination:Destination;
  @ViewChild('dataForm') dataForm: NgForm;
  Index:number =0;
  newDescription:boolean = false;
  user:User;
  uploader:FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;


  constructor(private destinationService:DestinationsService, private route: ActivatedRoute,
    private toastr:ToastrService, private accountService:AccountService,
    private router:Router) {
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user)
     }

  ngOnInit(): void {
    this.initializeUploader();
  }

  addNewDescription(){
    this.newDescription = true;
  }

  updateDescription(descriptionId:number){
    this.destinationService.updateDescription(this.destination.descriptions[descriptionId]).subscribe({
      next:() => {this.toastr.success('Descrição atualizada');
      this.dataForm.reset(this.destination.descriptions[descriptionId]);
    }
    })
  }

  fileOverBase(e:any){
    this.hasBaseDropZoneOver =e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
    url:this.baseUrl + 'destination/description/add-photo/' + this.destination.descriptions[this.Index].id,
    authToken:'Bearer ' + this.user.token,
    isHTML5:true,
    allowedFileType:['image'],
    removeAfterUpload:true,
    autoUpload:false,
    maxFileSize:10 * 1024 * 1024
  });

  this.uploader.onAfterAddingFile = (file) =>{
    file.withCredentials = false;
  }
  this.uploader.onSuccessItem = (item,response, status, headers) =>{
    if(response) {
      const photo:DescriptionPhoto = JSON.parse(response);
      this.destination.descriptions[this.Index].descriptionPhoto.url = photo.url;
      }
    }
  }
  getIndex(index:any){
    this.Index= index;
  }

  deleteDescriptionPhoto (index:number){
    this.destinationService.deleteDescriptionPhoto(this.destination.descriptions[index].id).subscribe({
      next:()=> {this.toastr.success('Foto deletada');
      this.reloadCurrentRoute();
  }
    })
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
}


}


