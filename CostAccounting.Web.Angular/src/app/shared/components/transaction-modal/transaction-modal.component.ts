import { Component, OnInit } from "@angular/core";
import { ModalDismissReasons, NgbModal } from "@ng-bootstrap/ng-bootstrap";

@Component({
    selector: "app-transaction-modal",
    templateUrl: "transaction-modal.component.html",
})
export class TransactionModalComponent implements OnInit {
    closeResult = "";

    ngOnInit() {}

    constructor(private modalService: NgbModal) {}

    open(content) {
        this.modalService.open(content, { ariaLabelledBy: "modal-basic-title" }).result.then(
            (result) => {
                this.closeResult = `Closed with: ${result}`;
            },
            (reason) => {
                this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
            }
        );
    }

    private getDismissReason(reason: any): string {
        if (reason === ModalDismissReasons.ESC) {
            return "by pressing ESC";
        } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
            return "by clicking on a backdrop";
        } else {
            return `with: ${reason}`;
        }
    }
}
