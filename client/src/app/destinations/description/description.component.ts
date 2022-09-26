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
  model:any={};
  Index:number =0;
  newDescription:boolean = false;
  user:User;
  baseUrl = environment.apiUrl;
  shortLink:string = "";
  loading:boolean = false;
  file:File = null;

  constructor(private destinationService:DestinationsService, private route: ActivatedRoute,
    private toastr:ToastrService, private accountService:AccountService,
    private router:Router) {
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user)
     }

  ngOnInit(): void {
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
  registerDescriptions() {
    this.destinationService.registerDescription(this.model,this.destination.name).subscribe({
      next:()=>{this.toastr.success('Registrado')
      this.reloadCurrentRoute();
    }
    })
  }

  onChange(event) {
    this.file =event.target.files[0];
  }

  onUpload(descriptionId:number) {
    this.loading = !this.loading;
    this.destinationService.uploadDescriptionPhoto(this.file,descriptionId).subscribe({
      next:(event:any)=>{
        if(typeof (event) === 'object'){
          this.loading = false;
          this.reloadCurrentRoute();
        }
      }, error:(e)=>console.log(e)
    });
  }

}


