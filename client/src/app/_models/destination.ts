import { DestinationPhoto } from './destinationPhoto';

export interface Destination{
  id:number;
  name:string;
  public:boolean;
  photoUrl:string;
  title1:string;
  title2:string;
  title3:string;
  title4:string;
  title5:string;
  description1:string;
  description2:string;
  description3:string;
  description4:string;
  description5:string;
  price:number;
  created:Date;
  city:string;
  hotel:string;
  country:string;
  bestMonth:string;
  destinationPhotos:DestinationPhoto[];

}
