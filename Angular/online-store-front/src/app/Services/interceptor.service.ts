import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { HttpInterceptor } from '@angular/common/http';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private router: Router) { }

    intercept(req, next) {
        if (req.headers.get('No-Auth') == "True") {
            return next.handle(req);
        }
        if (localStorage.getItem('token') != null) {
            const clonedreq = req.clone({
                headers: req.headers.set("Authorization", "Bearer " + localStorage.getItem('token'))
            });
            return next.handle(clonedreq);
        }
        else {
            this.router.navigate[("ClientLogin")];
        }
        return next.handle(req);
    }
}