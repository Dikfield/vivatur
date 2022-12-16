import { PromotionPhoto } from './promotionPhoto';

export interface Promotion{
  id:number;
  name:string;
  description:string;
  public:boolean;
  photoUrl:string;
  price:number;
  created:Date;
  city:string;
  bestMonths:string;
  promotionPhotos:PromotionPhoto[];

}
