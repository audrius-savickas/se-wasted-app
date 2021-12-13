import {createReducer} from "@reduxjs/toolkit"
import {setUserId} from "../actions/user"

interface State {
  userId: string
}

export const initialState: State = {
  userId: ""
}

export const user = createReducer(initialState, builder => {
  builder.addCase(setUserId, (state, {payload}) => {
    state.userId = payload.userId
  })
})
