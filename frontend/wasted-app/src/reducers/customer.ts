import {createReducer} from "@reduxjs/toolkit"
import {logOutCustomer, setCustomerId} from "../actions/customer"

interface State {
  customerId: string
}

export const initialState: State = {
  customerId: ""
}

export const customer = createReducer(initialState, builder => {
  builder.addCase(setCustomerId, (state, {payload}) => {
    state.customerId = payload.customerId
  })
  builder.addCase(logOutCustomer, state => {
    state.customerId = initialState.customerId
  })
})
