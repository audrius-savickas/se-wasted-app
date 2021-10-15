import React from "react"
import {Text, View} from "react-native-ui-lib"

interface OurComponentProps {
  text: string
  number: string
}

export const OurComponent = ({text, number}: OurComponentProps) => {
  return (
    <View>
      <Text text10>Hello {text}</Text>
      <Text>Hello {number}</Text>
    </View>
  )
}
