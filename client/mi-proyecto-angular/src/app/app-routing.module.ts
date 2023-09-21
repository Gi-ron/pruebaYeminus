import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MenuComponent } from './menu/menu.component';
// import { EncriptarComponent } from './encriptar/encriptar.component';
// import { DesencriptarComponent } from './desencriptar/desencriptar.component';
// import { FibonacciComponent } from './fibonacci/fibonacci.component';
// import { ProductosComponent } from './productos/productos.component';

const routes: Routes = [
  { path: '', component: MenuComponent },
  // { path: 'encriptar', component: EncriptarComponent },
  // { path: 'desencriptar', component: DesencriptarComponent },
  // { path: 'fibonacci', component: FibonacciComponent },
  // { path: 'productos', component: ProductosComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
