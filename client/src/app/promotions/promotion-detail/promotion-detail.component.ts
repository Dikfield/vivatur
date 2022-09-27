import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Destination } from 'src/app/_models/destination';
import { Promotion } from 'src/app/_models/promotion';
import { DestinationsService } from 'src/app/_services/destinations.service';
import { PromotionsService } from 'src/app/_services/promotions.service';


@Component({
  selector: 'app-promotion-detail',
  templateUrl: './promotion-detail.component.html',
  styleUrls: ['./promotion-detail.component.css']
})
export class PromotionDetailComponent implements OnInit {
  promotion:Promotion;

  constructor(private promotionService:PromotionsService,
    private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.loadPromotion();

  }

  loadPromotion() {
    this.promotionService.getPromotionById(parseInt(this.route.snapshot.paramMap.get('id'))).subscribe({
      next:(promotion) => this.promotion = promotion
    })
  }

}
