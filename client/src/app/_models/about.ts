import { VivaPhoto } from './vivaPhotos';
import { Feedback } from './feedback';

export interface About{
  id:number;
  title:string;
  description:string;
  vivaInfo:boolean;
  vivaPhotos:VivaPhoto[];
  feedbacks:Feedback[];

}
