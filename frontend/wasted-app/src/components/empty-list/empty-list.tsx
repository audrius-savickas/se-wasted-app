import React from "react"
import {StateScreen, View} from "react-native-ui-lib"
import {EmptyListProps} from "./interfaces"

export const EmptyList = ({title, subtitle, buttonLabel, onPress}: EmptyListProps) => {
  return (
    <View flex marginH-s6>
      <StateScreen title={title} subtitle={subtitle} ctaLabel={buttonLabel} onCtaPress={onPress} />
    </View>
  )
}
