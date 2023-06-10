import { Pipe, PipeTransform } from "@angular/core";
import { Product } from "../model/porduct.model";
import { CategoryList } from "../model/category.model";




@Pipe({
    name : 'productFilter'
})

export class ProductFilter implements PipeTransform{
     category: CategoryList 

    transform(product:Product[], selectedCategory:string) {
        if(!selectedCategory || selectedCategory === "All"){
            return product;
        }
        return product.filter(product=>product.categoryId === selectedCategory )
        
    }
}