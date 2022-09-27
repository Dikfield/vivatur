import { PromotionDescription } from './promotionDescription';
import { PromotionPhoto } from './promotionPhoto';

export interface Promotion{
  id:number;
  name:string;
  public:boolean;
  photoUrl:string;
  price:number;
  created:Date;
  city:string;
  bestMonths:string;
  promotionPhotos:PromotionPhoto[];
  promotionDescriptions:PromotionDescription[];

}
