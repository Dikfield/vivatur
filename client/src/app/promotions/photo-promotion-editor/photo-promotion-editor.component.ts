import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Destination } from 'src/app/_models/destination';
import { DestinationPhoto } from 'src/app/_models/destinationPhoto';
import { DestinationsService } from 'src/app/_services/destinations.service';
import { environment } from 'src/environments/environment';
import { FileUploader, FileUploadModule } from 'ng2-file-upload';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs';
import { Promotion } from 'src/app/_models/promotion';
import { PromotionsService } from 'src/app/_services/promotions.service';

@Component({
  selector: 'app-photo-promotion-editor',
  templateUrl: './photo-promotion-editor.component.html',
  styleUrls: ['./photo-promotion-editor.component.css']
})
export class PhotoPromotionEditorComponent implements OnInit {
  @Input() promotion: Promotion;
  user:User;
  uploader:FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;

  constructor(private promotionService:PromotionsService, private route: ActivatedRoute,
    private toastr:ToastrService, private accountService:AccountService) {
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user)
     }

  ngOnInit(): void {
    this.initializeUploader();
  }

  setMainPhoto(id:number, photo:DestinationPhoto) {
    this.promotionService.setMainPhoto(id, photo.id).subscribe(() => {
      this.promotion.photoUrl = photo.url;
      this.promotion.promotionPhotos.forEach(p =>{
        if(p.isMain)p.isMain = false;
        if(p.id ===photo.id) p.isMain = true;
      })
    })
  }

  deletePhoto(photoId:number){
    this.promotionService.deletePhoto(photoId).subscribe(()=>{
      this.promotion.promotionPhotos = this.promotion.promotionPhotos.filter(x=>x.id !== photoId);
    })
  }
  fileOverBase(e:any){
    this.hasBaseDropZoneOver =e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
    url:this.baseUrl + 'promotion/add-photo/' + this.promotion.id,
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
      const photo:DestinationPhoto = JSON.parse(response);
      this.promotion.promotionPhotos.push(photo);
      if(photo.isMain) {
        this.promotion.photoUrl = photo.url;
        this.accountService.setCurrentUser(this.user);
      }
    }
  }
}

}

