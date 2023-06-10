import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Category, CategoryList } from 'src/app/model/category.model';
import { Product } from 'src/app/model/porduct.model';
import { CategoryService } from 'src/app/service/category.service';
import { ProductService } from 'src/app/service/product.service';

@Component({
  selector: 'app-edit-add-product',
  templateUrl: './edit-add-product.component.html',
  styleUrls: ['./edit-add-product.component.css']
})
export class EditAddProductComponent implements OnInit {
  myForm:FormGroup;
  categories:CategoryList[];
  product:Product;
  isEdit:boolean ;
  productImage:string;
  imgChange:boolean = false;
  formData:FormData = new FormData();

  constructor(private productService:ProductService, private route:ActivatedRoute, private fb:FormBuilder , private categoryService:CategoryService){}
ngOnInit(): void {
  this.categoryService.getCategoryList().subscribe(data=>{this.categories=data})
  this.product = this.route.snapshot.data["data"];
  this.isEdit = this.product != null
  this.initForm(this.product) 
}

UpdateProduct(){
  Object.keys(this.myForm.value).forEach((key) => {
    this.formData.set(key, this.myForm.value[key]);
  });
  this.productService.updateProduct(this.product.productId , this.formData)
}
AddProduct(){
  Object.keys(this.myForm.value).forEach((key) => {
    this.formData.set(key, this.myForm.value[key]);
  });
  this.productService.addProduct(this.formData)
}

private initForm(data){
  let name = "";
  let price = undefined;
  let quantity = undefined;
  let discount = undefined;
  let description = undefined;
  let categoryId:string 
  let image = '';
  if(this.isEdit){
    name = data.name;
    price = data.price;
    quantity = data.quantity;
    discount = data.discount;
    description =data.description
    categoryId = data.categoryId
    image = data.image
  }
  this.myForm = this.fb.group({
    categoryId:[categoryId],
    name:[name],
    price:[price],
    quantity:[quantity],
    discount:[discount],
    description:[description],
    image:[image],
  })
}

onFileSelected(event){
  //console.log(event)
  const selectedFile = <File>event.target.files[0];
  this.formData.append('fileImg',selectedFile,selectedFile.name);
  this.imgChange=true
  if(this.isEdit || this.imgChange){
    const reader = new FileReader();
  reader.onload = () => {
    this.productImage = reader.result as string;
  };
  reader.readAsDataURL(selectedFile);
  }
}




}
