import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Category, CategoryList } from '../model/category.model';
import { apiServer } from 'src/assets/config/config';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
 url = apiServer.URL + "api/categories";
 categoryChanged = new Subject<CategoryList[]>()

  constructor(private http:HttpClient , private toastr:ToastrService) { }
  
  getCategoryList() : Observable<CategoryList[]>{
    return this.http.get<CategoryList[]>(this.url);
  }

  getCategoryById(id:string){
    return this.http.get<CategoryList>(`${this.url}/${id}`)
  }

  deleteCategory(id:string){
    this.http.delete(`${this.url}/${id}`).subscribe(()=>{
      this.emitCategory();
        this.toastr.success('category deleted successfully')
    })
  }


  addCategory(category:Category){
    this.http.post<Category>(this.url,category).subscribe(()=>{
      this.emitCategory();
      this.toastr.success("sucess")
    })
  }

  editCategory(data:any){
    this.http.put(`${this.url}/${data.categoryId}`,data).subscribe(()=>{
      this.emitCategory();
      this.toastr.success("edit cat successfully")
    })
  }

  emitCategory(){
    this.getCategoryList().subscribe(data=>{
      this.categoryChanged.next(data)
    })
  }

}
