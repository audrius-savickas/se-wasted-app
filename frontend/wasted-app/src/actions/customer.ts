import {createAction} from "@reduxjs/toolkit"

export const setCustomerId = createAction<{customerId: string}>("SET_CUSTOMER_ID")

export const logOutCustomer = createAction("CUSTOMER_LOG_OUT")
