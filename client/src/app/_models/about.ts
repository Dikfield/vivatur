import { VivaPhoto } from './vivaPhotos';

export interface About{
  id:number;
  title:string;
  description:string;
  vivaInfo:boolean;
  vivaPhotos:VivaPhoto[];

}
