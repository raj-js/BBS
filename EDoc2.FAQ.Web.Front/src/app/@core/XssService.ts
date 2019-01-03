import * as xss from 'xss';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class XssService {
    options = {
        whiteList: {
            span: [ 'style' ],
            h1: [], h2: [], h3: [], h4: [], h5: [], h6: [],
            p: [],
            img: [],
            table: [],
            br: [],
            a: [ 'href', 'target' ]
        }
    };

    filter: any;

    constructor() {
        this.filter = new xss.FilterXSS(this.options);
    }

    public process(html: string): string {
        return this.filter.process(html);
    }
}
