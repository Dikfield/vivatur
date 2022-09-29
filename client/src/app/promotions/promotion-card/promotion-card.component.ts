import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Destination } from 'src/app/_models/destination';
import { Promotion } from 'src/app/_models/promotion';
import { DestinationsService } from 'src/app/_services/destinations.service';
import { PromotionsService } from 'src/app/_services/promotions.service';

@Component({
  selector: 'app-promotion-card',
  templateUrl: './promotion-card.component.html',
  styleUrls: ['./promotion-card.component.css'],

})
export class PromotionCardComponent implements OnInit {
  @Input() promotion:Promotion;
  @Output() promotionDeleted = new EventEmitter<number>();

  constructor(private router:Router, private toastr:ToastrService, private promotionService:PromotionsService) {}

  ngOnInit(): void {
  }


  deletePromotion(id:number) {
    this.promotionService.deletePromotion(id).subscribe({
      next:(response)=> {
        this.toastr.success();
        this.promotionDeleted.emit(id);
     }
  })
  }
}
