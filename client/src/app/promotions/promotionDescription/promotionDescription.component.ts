import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Promotion } from 'src/app/_models/promotion';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { PromotionsService } from 'src/app/_services/promotions.service';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-promotionDescription',
  templateUrl: './promotionDescription.component.html',
  styleUrls: ['./promotionDescription.component.css']
})
export class PromotionDescriptionComponent implements OnInit {
  @Input() promotion:Promotion;
  @ViewChild('dataForm') dataForm: NgForm;
  model:any={};
  Index:number =0;
  newDescription:boolean = false;
  user:User;
  baseUrl = environment.apiUrl;
  shortLink:string = "";
  loading:boolean = false;
  file:File = null;

  constructor(private promotionService:PromotionsService, private route: ActivatedRoute,
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
    this.promotionService.updateDescription(this.promotion.promotionDescriptions[descriptionId]).subscribe({
      next:() => {this.toastr.success('Descrição atualizada');
      this.dataForm.reset(this.promotion.promotionDescriptions[descriptionId]);
    }
    })
  }

  getIndex(index:any){
    this.Index= index;
  }

  deleteDescriptionPhoto (index:number){
    this.promotionService.deleteDescriptionPhoto(this.promotion.promotionDescriptions[index].id).subscribe({
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
    this.promotionService.registerDescription(this.model,this.promotion.id).subscribe({
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
    this.promotionService.uploadDescriptionPhoto(this.file,descriptionId).subscribe({
      next:(event:any)=>{
        if(typeof (event) === 'object'){
          this.loading = false;
        }
        this.reloadCurrentRoute();
      }, error:(e)=>console.log(e)
    });
  }

}


