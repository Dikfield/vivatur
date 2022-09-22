import { DestinationDescription } from './destinationDescription';
import { DestinationPhoto } from './destinationPhoto';

export interface Destination{
  id:number;
  name:string;
  public:boolean;
  photoUrl:string;
  created:Date;
  city:string;
  bestMonths:string;
  destinationPhotos:DestinationPhoto[];
  descriptions:DestinationDescription[];

}
