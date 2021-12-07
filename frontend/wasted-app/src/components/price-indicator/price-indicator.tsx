import React, {useState} from "react"
import {LayoutChangeEvent, StyleSheet} from "react-native"
import {Colors, Spacings, Text, View} from "react-native-ui-lib"
import {formatPrice} from "../../utils/currency"
import {PriceIndicatorProps} from "./interfaces"

export const PriceIndicator = ({currentPrice, minimumPrice, maximumPrice}: PriceIndicatorProps) => {
  const [containerWidth, setContainerWidth] = useState(100)

  const progress = currentPrice === minimumPrice ? 1 : (currentPrice - minimumPrice) / (maximumPrice - minimumPrice)

  const getContainerWidth = (event: LayoutChangeEvent) => {
    setContainerWidth(event.nativeEvent.layout.width)
  }

  const getProgressStyle = () => {
    return StyleSheet.create({
      style: {
        left: containerWidth * progress
      }
    }).style
  }

  return (
    <>
      <View br30 bg-green20 centerV style={styles.outer} onLayout={getContainerWidth}>
        <View bg-red20 style={[styles.progress, getProgressStyle()]}></View>
      </View>
      <View row marginT-2>
        <Text flex>Min: {formatPrice(minimumPrice)}</Text>
        <Text>Max: {formatPrice(maximumPrice)}</Text>
      </View>
    </>
  )
}

const styles = StyleSheet.create({
  outer: {
    overflow: "hidden",
    borderColor: Colors.grey20,
    borderWidth: 1,
    height: Spacings.s4,
    opacity: 0.8
  },
  progress: {
    position: "absolute",
    height: "100%",
    width: "100%",
    borderColor: Colors.grey20,
    borderLeftWidth: 1,
    alignItems: "center",
    justifyContent: "center",
    overflow: "hidden"
  }
})
