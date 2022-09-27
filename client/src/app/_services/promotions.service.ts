import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Promotion } from '../_models/promotion';
import { PromotionDescription } from '../_models/promotionDescription';


@Injectable({
  providedIn: 'root'
})
export class PromotionsService {
  baseUrl = environment.apiUrl;
  promotions: Promotion [] = [];
  promotionDescriptions:PromotionDescription [] = [];


  constructor(private http:HttpClient) { }

  registerPromotion(model:any){
    return this.http.post(this.baseUrl + 'promotion', model).pipe(
      map((promotion : any) => {
        console.log(promotion);
      })
    )
  }

  uploadDescriptionPhoto(file, descriptionId:number):Observable<any>{
    const formData = new FormData();
    formData.append("file", file, file.name);
    return this.http.post(this.baseUrl + 'promotion/description/add-photo/' + descriptionId, formData);
  }

  registerDescription(model:any, id:number){
    return this.http.post(this.baseUrl + 'promotion/description/register/' + id, model).pipe(
      map((promotionDescription : any) => {
        console.log(promotionDescription);
      })
    )
  }

  getPromotions() {
    if(this.promotions.length > 0) return of(this.promotions);
    return this.http.get<Promotion[]>(this.baseUrl + 'promotion').pipe(
      map(promotions =>{
        this.promotions = promotions;
        return promotions;
      })
    )
  }

  getPromotion(name:string) {
    const promotion = this.promotions.find(x => x.name === name);
    if(promotion !== undefined) return of(promotion);
    return this.http.get<Promotion>(this.baseUrl + 'promotion/' + name);
  }

  getPromotionById(id:number) {
    const promotion = this.promotions.find(x => x.id === id);
    if(promotion !== undefined) return of(promotion);
    return this.http.get<Promotion>(this.baseUrl + 'promotion/' + id);
  }

  updatePromotion(promotion:Promotion){
    return this.http.put(this.baseUrl + 'promotion/' + promotion.id, promotion).pipe(
      map(() => {
        const index = this.promotions.indexOf(promotion);
        this.promotions[index] = promotion;
      })
    )
  }

  updateDescription(description:PromotionDescription){
    return this.http.put(this.baseUrl + 'promotion/description/update/' + description.id, description).pipe(
      map(() => {})
    )
  }

  setMainPhoto(id:number, photoId:number) {
    return this.http.put(this.baseUrl + 'promotion/set-main-photo/'+ id +'/' + photoId, {})
  }

  deletePhoto(photoId:number){
    return this.http.delete(this.baseUrl + 'promotion/delete-photo/' + photoId);
  }

  deleteDescriptionPhoto(descriptionId:number) {
    return this.http.delete(this.baseUrl + 'promotion/description/delete-photo/' + descriptionId)
  }

  deletePromotion(id:number) {
    return this.http.delete(this.baseUrl + 'promotion/delete/' + id)
  }
}
