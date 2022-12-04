import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { About } from 'src/app/_models/about';
import { User } from 'src/app/_models/user';
import { VivaPhoto } from 'src/app/_models/vivaPhotos';
import { AboutService } from 'src/app/_services/about.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-about-edit',
  templateUrl: './about-edit.component.html',
  styleUrls: ['./about-edit.component.css']
})
export class AboutEditComponent implements OnInit {
  abouts:About[] = [];
  Index:number =0;
  photo:VivaPhoto;
  @ViewChild('dataForm') dataForm: NgForm;
  user:User;
  baseUrl = environment.apiUrl;
  shortLink:string = "";
  loading:boolean = false;
  file:File = null;


  constructor(private aboutService:AboutService, private toastr:ToastrService) { }

  ngOnInit(): void {
    this.loadAbouts();
  }

  loadAbouts() {
    this.aboutService.getAbouts().subscribe({
        next:(abouts) =>{
          this.abouts = abouts;
      }
    })
    }
    updateAbout() {
      this.aboutService.updateAbout(this.abouts[this.Index]).subscribe({
        next:(response) => {
        this.toastr.success('Descrição atualizada');
        this.dataForm.reset(this.abouts[0]);
      }
      })
    }

  getIndex(index:number){
    this.Index = index;
  }
  deletePhoto(photoId:number){
    this.aboutService.deletePhoto(photoId).subscribe({
      next:()=>{
        this.toastr.success;
        this.abouts[this.Index].vivaPhotos = this.abouts[this.Index].vivaPhotos.filter(x=> x.id !== photoId)
        }
    })
  }

  onChange(event) {
    this.file =event.target.files[this.Index];
  }

  onUpload(aboutId:number) {
    this.loading = !this.loading;
    this.aboutService.uploadAboutPhoto(this.file,aboutId).subscribe({
      next:(event:any)=>{
        if(typeof (event) === 'object'){
          this.loading = false;
          this.abouts[this.Index].vivaPhotos.push(event);
        }

      }, error:(e)=>console.log(e)
    });
  }

  }
