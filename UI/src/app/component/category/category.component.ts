import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CategoryItem, CategoryList } from 'src/app/model/category.model';
import { Product } from 'src/app/model/porduct.model';
import { CategoryService } from 'src/app/service/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  categoryList:CategoryList[];
  myForm:FormGroup;
  isEdit:boolean = false;
  editCat:CategoryItem

  constructor(private catService:CategoryService, private fb:FormBuilder){}
  ngOnInit(): void {
    this.catService.getCategoryList().subscribe(data=>{
      this.categoryList = data
    })
    this.catService.categoryChanged.subscribe(d=>{
      this.categoryList = d
    })

    this.initForm();
  }

  AddCategory(){
    this.catService.addCategory(this.myForm.value)
    this.myForm.reset()
  }

  EditCategory(){
    this.catService.editCategory(this.myForm.value);
    this.isEdit = false;
    this.myForm.reset()
  }

  deleteCategory(data:string){
    this.catService.deleteCategory(data)
  }

editCategory(data){
  this.myForm = this.fb.group({
    categoryId :[data.categoryId],
    name:[data.name],
    icon:[data.icon]
  })
  this.isEdit = true;
}
addCategory(){
  this.isEdit = false;
  this.myForm.reset()
}

  private initForm(){
    this.myForm = this.fb.group({
      name:[''],
      icon:['']
    })
  }
}
