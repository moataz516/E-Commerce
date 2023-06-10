
export class Product{
    productId :string;
    categoryId:string;
    name : string ;
    price:number;
    discount:number;
    quantity:number;
    description: string;
    image:string
    createdAt:string
}



export class ProductSend{
    name : string ;
    price:number;
    quantity:number;
    description: string;
}

export class AddProduct{
    categoryId:string;
    name : string ;
    price:number;
    discount:number;
    quantity:number;
    description: string;
    image:string
}