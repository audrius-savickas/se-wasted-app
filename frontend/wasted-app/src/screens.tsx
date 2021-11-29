import React from "react"
import {Provider} from "react-redux"
import {store} from "./store"

export function withProvider<Props>(Component: React.ComponentType<Props>) {
  return (props: Props) => (
    <Provider store={store}>
      <Component {...props} />
    </Provider>
  )
}
