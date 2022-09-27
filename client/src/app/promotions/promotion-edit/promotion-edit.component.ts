import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Promotion } from 'src/app/_models/promotion';
import { PromotionsService } from 'src/app/_services/promotions.service';

@Component({
  selector: 'app-promotion-edit',
  templateUrl: './promotion-edit.component.html',
  styleUrls: ['./promotion-edit.component.css']
})
export class PromotionEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  promotion:Promotion;
  bottom:boolean;
  model:any = {};

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event:any){
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  }

  constructor(private promotionService:PromotionsService, private route: ActivatedRoute,
    private toastr:ToastrService) {

   }

  ngOnInit(): void {
    this.loadPromotion();
    this.bottom = false;

  }

  loadPromotion() {
    this.promotionService.getPromotionById(parseInt(this.route.snapshot.paramMap.get('id'))).subscribe({
      next:(promotion) => this.promotion = promotion
    })
  }
  updatePromotion(){
    if(this.model.public ==="1") this.promotion.public = true;
    else this.promotion.public = false;
    this.bottom=false;
      this.promotionService.updatePromotion(this.promotion).subscribe({
      next:() => {this.toastr.success('Promoção atualizada');
      this.editForm.reset(this.promotion);
    }
    })
  }

  clickBottom(){
    this.bottom = !this.bottom;
  }

}
